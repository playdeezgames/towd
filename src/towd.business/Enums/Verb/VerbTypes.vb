Imports System.Runtime.CompilerServices
Imports towd.data

Public Module VerbTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of VerbType, IVerbType) =
        New List(Of IVerbType) From
        {
            New TwineVerbTypeDescriptor(),
            New SharpRockVerbTypeDescriptor(),
            New SharpStickVerbTypeDescriptor(),
            New HatchetVerbTypeDescriptor(),
            New HammerVerbTypeDescriptor(),
            New PlankVerbTypeDescriptor(),
            New CookingFireVerbTypeDescriptor(),
            New FurnaceVerbTypeDescriptor(),
            New CharcoalCookingFireVerbTypeDescriptor(),
            New CharcoalFurnaceVerbTypeDescriptor(),
            New CookedGrubVerbTypeDescriptor(),
            New CookedFishFiletVerbTypeDescriptor(),
            New UnfiredBrickVerbTypeDescriptor(),
            New KnifeVerbTypeDescriptor(),
            New BladeVerbTypeDescriptor(),
            New RawFishFiletVerbTypeDescriptor(),
            New FishingNetVerbTypeDescriptor(),
            New BrickVerbTypeDescriptor(),
            New ForageGrassVerbTypeDescriptor(),
            New ForageRockVerbTypeDescriptor(),
            New ForagePineVerbTypeDescriptor(),
            New WaitVerbTypeDescriptor(),
            New ChopVerbTypeDescriptor(),
            New DigGrassVerbTypeDescriptor(),
            New DigPondVerbTypeDescriptor(),
            New FishVerbTypeDescriptor(),
            New EatCookedGrubVerbTypeDescriptor(),
            New EatCookedFishFiletVerbTypeDescriptor(),
            New AddFuelCookingFireStickVerbTypeDescriptor(),
            New AddFuelCookingFirePlankVerbTypeDescriptor(),
            New AddFuelCookingFireLogVerbTypeDescriptor(),
            New AddFuelCookingFireCharcoalVerbTypeDescriptor(),
            New AddFuelFurnaceCharcoalVerbTypeDescriptor()
        }.
        ToDictionary(Function(x) x.VerbType, Function(x) x)
    <Extension>
    Public Function ToVerbTypeDescriptor(verbType As VerbType) As IVerbType
        Return Descriptors(verbType)
    End Function

End Module
