//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//

using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Sharing.SyncModel;

namespace FeelPhysics.HoloMagnet36
{
    /// <summary>
    /// Class that demonstrates a custom class using sync model attributes.
    /// </summary>
    [SyncDataClass]
    public class SyncSpawnedGlobalParams : SyncSpawnedObject
    {
        [SyncData]
        public SyncBool ShouldShowMagneticForceLines;

        [SyncData]
        public SyncBool ShouldShowDebugLog;

        [SyncData]
        public SyncBool ShouldShowCompass2D;

        [SyncData]
        public SyncBool ShouldShowCompass3D;

        [SyncData]
        public SyncBool ShouldHoldBarMagnetZPosition;
    }
}