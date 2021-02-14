#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class ResourcePrefabPathBuilder : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        //NetworkingManager.PopulatePrefabsonNetwork();
    }
    
}
#endif