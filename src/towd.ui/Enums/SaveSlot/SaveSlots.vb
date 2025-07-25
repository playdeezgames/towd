﻿Imports System.Runtime.CompilerServices

Public Module SaveSlots
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of String, ISaveSlot) =
        New List(Of ISaveSlot) From
        {
            New SaveSlotDescriptor(SaveSlot.ScumSlot, "Scum Slot", "scum.json"),
            New SaveSlotDescriptor(SaveSlot.Auto, "Autosave Slot", "autosave.json"),
            New SaveSlotDescriptor(SaveSlot.Slot1, "Slot 1", "slot1.json"),
            New SaveSlotDescriptor(SaveSlot.Slot2, "Slot 2", "slot2.json"),
            New SaveSlotDescriptor(SaveSlot.Slot3, "Slot 3", "slot3.json"),
            New SaveSlotDescriptor(SaveSlot.Slot4, "Slot 4", "slot4.json"),
            New SaveSlotDescriptor(SaveSlot.Slot5, "Slot 5", "slot5.json")
        }.ToDictionary(Function(x) x.SaveSlot, Function(x) x)
    <Extension>
    Public Function ToSaveSlotDescriptor(saveSlot As String) As ISaveSlot
        Return Descriptors(saveSlot)
    End Function
End Module
