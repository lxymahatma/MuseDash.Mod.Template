global using HarmonyLib;
global using MelonLoader;
global using MelonLoader.Utils;
#if UseMuseDashMirror
global using MuseDashMirror.Attributes;
global using MuseDashMirror.Attributes.EventAttributes.PatchEvents;
global using MuseDashMirror.Attributes.EventAttributes.SceneEvents;
global using MuseDashMirror.EventArguments;
global using MuseDashMirror.Extensions.UnityExtensions;
#endif
#if UseUniTask
global using Cysharp.Threading.Tasks;
global using Cysharp.Threading.Tasks.Linq;
#endif
global using static MuseDashModTemplate.MelonBuildInfo;
#if UseMuseDashMirror
global using static MuseDashMirror.UI.Colors;
global using static MuseDashMirror.UI.Fonts;
global using static MuseDashMirror.Utils.GameObjectUtils;
#endif
