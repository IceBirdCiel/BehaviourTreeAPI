using System;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using PGSauce.Save;
using PGSauce.Unity;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using LoginResult = PlayFab.ClientModels.LoginResult;

namespace PGSauce.Playfab
{
    public class PlayFabStartup : MonoSingleton<PlayFabStartup>
    {
        [SerializeField] private SavedDataPlayFabAuthType savedAuthType;
        [SerializeField] private SavedDataBool rememberMe;
        [SerializeField] private SavedDataString rememberMeId;

        /// <summary>
        /// How to authenticate
        /// </summary>
        public enum AuthTypes
        {
            /// <summary>
            /// When nothing is set up yet
            /// </summary>
            None,
            /// <summary>
            /// Anonymous auth, per device, not recoverable
            /// </summary>
            Silent,
            /// <summary>
            /// Auth with an email and password
            /// </summary>
            EmailAndPassword,
            /// <summary>
            /// Auth with Facebook
            /// </summary>
            Facebook,
            /// <summary>
            /// Auth with Google
            /// </summary>
            Google
        }
        
        private PlayFabHandlerBase _handler;
        private string _playFabId;
        private string _sessionTicket;
        public GetPlayerCombinedInfoRequestParams InfoRequestParameters { get; private set; }

        public void InitPlayFab(bool resetPlayFab, GetPlayerCombinedInfoRequestParams requestParams)
        {
            InfoRequestParameters = requestParams;
            CreateHandler();
            if (resetPlayFab)
            {
                try
                {
                    _handler.UnlinkSilentAuth();
                }
                catch (Exception e)
                {
                    PGDebug.Message($"{e.Message}").LogWarning();
                }
                savedAuthType.ResetSave();
                rememberMe.ResetSave();
                rememberMeId.ResetSave();
            }

            Authenticate();
        }
        
        /// <summary>
        /// Create a new Playfab account
        /// </summary>
        /// <param name="email">The registered email</param>
        /// <param name="password">The password</param>
        /// <param name="successCallback">On login success</param>
        /// <param name="errorCallback">On login error</param>
        public void RegisterPlayFabAccount(string email, string password, Action<LoginResult> successCallback, Action<PlayFabError> errorCallback)
        {
            // Any time we attempt to register a player, first silently authenticate the player.
            // This will retain the players True Origination (Android, iOS, Desktop)
            SilentlyAuthenticate(
                result => 
                {
                    if (result == null)
                    {
                        errorCallback?.Invoke(new PlayFabError()
                        {
                            Error = PlayFabErrorCode.UnknownError,
                            ErrorMessage = "Silent Authentication by Device failed"
                        });
                        return;
                    }
                    
                    // Note: If silent auth is success, which is should always be and the following 
                    // below code fails because of some error returned by the server ( like invalid email or bad password )
                    // this is okay, because the next attempt will still use the same silent account that was already created.
                    var request = new AddUsernamePasswordRequest()
                    {
                        Username = result.PlayFabId,
                        Email = email,
                        Password = password
                    };
                    
                    PlayFabClientAPI.AddUsernamePassword(request,
                        addResult =>
                        {
                            UpdateData(result);
                            TryRememberMe();

                            savedAuthType.SaveData(AuthTypes.EmailAndPassword);
                            successCallback?.Invoke(result);
                            PGDebug.Message($"Success Register Playfab id {_playFabId}").Log();
                        },
                        error =>
                        {
                            errorCallback?.Invoke(error);
                            LogError(error);
                        });
                },
                error =>
                {
                    errorCallback?.Invoke(error);
                    LogError(error);
                });
        }

        public void LoginPlayFabAccount(string email, string password, Action<LoginResult> loginSuccess, Action<PlayFabError> loginError)
        {
            var request = new LoginWithEmailAddressRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                Email = email,
                Password = password,
                InfoRequestParameters = InfoRequestParameters
            };

            PlayFabClientAPI.LoginWithEmailAddress(request,
                result =>
                {
                    UpdateData(result);
                    TryRememberMe();
                    
                    savedAuthType.SaveData(AuthTypes.EmailAndPassword);
                    loginSuccess?.Invoke(result);
                    PGDebug.Message($"Success LOG IN Playfab id {_playFabId}").Log();
                }, 
                error =>
                {
                    loginError?.Invoke(error);
                    LogError(error);
                });
        }

        private void Authenticate()
        {
            var auth = savedAuthType.Load();
            switch (auth)
            {
                case AuthTypes.None:
                    savedAuthType.SaveData(AuthTypes.Silent);
                    SilentlyAuthenticate();
                    break;
                case AuthTypes.Silent:
                    SilentlyAuthenticate();
                    break;
                case AuthTypes.EmailAndPassword:
                    AuthenticateEmailPassword();
                    break;
                case AuthTypes.Facebook:
                    AuthenticateFacebook();
                    break;
                case AuthTypes.Google:
                    AuthenticateGoogle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AuthenticateGoogle()
        {
            throw new NotImplementedException();
        }

        private void AuthenticateFacebook()
        {
            throw new NotImplementedException();
        }
        private void AuthenticateEmailPassword()
        {
            if (rememberMe.Load() && !string.IsNullOrEmpty(rememberMeId.Load()))
            {
                var request = new LoginWithCustomIDRequest()
                {
                    TitleId = PlayFabSettings.TitleId,
                    CustomId = rememberMeId.Load(),
                    CreateAccount = false,
                    InfoRequestParameters = InfoRequestParameters
                };
                
                PlayFabClientAPI.LoginWithCustomID(request,
                    result =>
                    {
                        UpdateData(result);
                        PGDebug.Message($"Success Login Startup id {_playFabId}").Log();
                    },
                    error =>
                    {
                        LogError(error);
                    });
                
                return;
            }
            
            SilentlyAuthenticate();
        }

        private void SilentlyAuthenticate(Action<LoginResult> callback = null, Action<PlayFabError> errorCallback = null)
        {
            void SuccessCallback(LoginResult result)
            {
                //Store Identity and session
                UpdateData(result);

                callback?.Invoke(result);

                PGDebug.Message($"Success Login SILENT. new : {result.NewlyCreated}, id : {_playFabId}").Log();
            }

            void ErrorCallback(PlayFabError error)
            {
                //make sure the loop completes, callback with null
                errorCallback?.Invoke(null);
                //Output what went wrong to the console.
                LogError(error);
            }

            _handler.SilentlyAuthenticate(this, SuccessCallback, ErrorCallback);
        }

        private static void LogError(PlayFabError error)
        {
            PGDebug.Message(error.GenerateErrorReport()).LogError();
        }

        private void UpdateData(LoginResult result)
        {
            _playFabId = result.PlayFabId;
            _sessionTicket = result.SessionTicket;
        }
        
        private void TryRememberMe()
        {
            if (rememberMe.Load())
            {
                rememberMeId.SaveData(Guid.NewGuid().ToString());

                PlayFabClientAPI.LinkCustomID(new LinkCustomIDRequest()
                {
                    CustomId = rememberMeId.Load(),
                    ForceLink = false
                }, null, null);
            }
        }

        private void CreateHandler()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _handler = new PlayFabAndroidHandler();
#elif UNITY_IPHONE || UNITY_IOS && !UNITY_EDITOR
            _handler = new PlayFabiOSHandler();
#else
            _handler = new PlayFabOtherHandler();
#endif
        }
    }
}