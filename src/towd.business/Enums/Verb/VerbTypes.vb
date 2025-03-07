﻿Imports System.Runtime.CompilerServices
Imports towd.data

Public Module VerbTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of VerbType, IVerbType) =
        New List(Of IVerbType) From
        {
            New ForageVerbTypeDescriptor(),
            New CraftVerbTypeDescriptor(),
            New ChopVerbTypeDescriptor(),
            New DigVerbTypeDescriptor(),
            New EatGrubVerbTypeDescriptor(),
            New EatFishVerbTypeDescriptor(),
            New AddFuelVerbTypeDescriptor(),
            New WaitVerbTypeDescriptor(),
            New FishVerbTypeDescriptor()
        }.ToDictionary(Function(x) x.VerbType, Function(x) x)
    <Extension>
    Friend Function ToDescriptor(verbType As VerbType) As IVerbType
        Return Descriptors(verbType)
    End Function
End Module
