﻿Imports System.Runtime.CompilerServices
Imports towd.data

Public Module ItemTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, IItemType) =
        New List(Of IItemType) From
        {
            New PlantFiberItemTypeDescriptor(),
            New StickItemTypeDescriptor(),
            New RockItemTypeDescriptor(),
            New TwineItemTypeDescriptor(),
            New SharpRockItemTypeDescriptor(),
            New HatchetItemTypeDescriptor(),
            New LogItemTypeDescriptor(),
            New HammerItemTypeDescriptor(),
            New PlankItemTypeDescriptor(),
            New SharpStickItemTypeDescriptor(),
            New GrubItemTypeDescriptor(),
            New CookedGrubItemTypeDescriptor(),
            New ClayItemTypeDescriptor(),
            New CharcoalItemTypeDescriptor(),
            New UnfiredBrickItemTypeDescriptor(),
            New BrickItemTypeDescriptor(),
            New FishingNetItemTypeDescriptor(),
            New RawFishItemTypeDescriptor(),
            New RawFishFiletItemTypeDescriptor(),
            New FishHeadItemTypeDescriptor(),
            New FishGutsItemTypeDescriptor(),
            New KnifeItemTypeDescriptor(),
            New BladeItemTypeDescriptor(),
            New CookedFishFiletItemTypeDescriptor(),
            New RockFlakeItemTypeDescriptor(),
            New CarrotItemTypeDescriptor()
        }.
        ToDictionary(Function(x) x.ItemType, Function(x) x)
    <Extension>
    Public Function ToItemTypeDescriptor(itemType As String) As IItemType
        Return Descriptors(itemType)
    End Function
End Module
