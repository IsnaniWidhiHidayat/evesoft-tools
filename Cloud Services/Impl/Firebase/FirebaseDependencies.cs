using System.Threading.Tasks;
using Firebase;
using UnityEngine;

namespace RollingGlory.FaceApp
{
    public static class FirebaseDependencies
    {
        #region private
        private static bool _isChecking,_isChecked;
        private static DependencyStatus _status;
        #endregion
         
        public static async Task<DependencyStatus> CheckAndFixDependencies()
        {
            if(_isChecking)
                await new WaitUntil(()=> _isChecked);

            if(!_isChecked) 
            {
                _isChecking = true;
                _status     = await FirebaseApp.CheckAndFixDependenciesAsync();
                _isChecking = false;
                _isChecked  = true;
            }

            return _status;
        }
    }
}