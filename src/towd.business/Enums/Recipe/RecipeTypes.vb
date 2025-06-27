Imports System.Runtime.CompilerServices
Imports towd.data

Public Module RecipeTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of VerbType, IRecipeType) =
        New List(Of IRecipeType) From
        {
            New TwineRecipeTypeDescriptor(),
            New SharpRockRecipeTypeDescriptor(),
            New SharpStickRecipeTypeDescriptor(),
            New HatchetRecipeTypeDescriptor(),
            New HammerRecipeTypeDescriptor(),
            New PlankRecipeTypeDescriptor(),
            New CookingFireRecipeTypeDescriptor(),
            New FurnaceRecipeTypeDescriptor(),
            New CharcoalCookingFireRecipeTypeDescriptor(),
            New CookedGrubRecipeTypeDescriptor(),
            New CookedFishFiletRecipeTypeDescriptor(),
            New UnfiredBrickRecipeTypeDescriptor(),
            New KnifeRecipeTypeDescriptor(),
            New BladeRecipeTypeDescriptor(),
            New RawFishFiletRecipeTypeDescriptor(),
            New FishingNetRecipeTypeDescriptor(),
            New BrickRecipeTypeDescriptor(),
            New ForageGrassRecipeTypeDescriptor(),
            New ForageRockRecipeTypeDescriptor(),
            New ForagePineRecipeTypeDescriptor(),
            New WaitRecipeTypeDescriptor(),
            New ChopRecipeTypeDescriptor(),
            New DigGrassRecipeTypeDescriptor(),
            New DigPondRecipeTypeDescriptor(),
            New FishRecipeTypeDescriptor(),
            New EatCookedGrubRecipeTypeDescriptor(),
            New EatCookedFishFiletRecipeTypeDescriptor(),
            New AddFuelCookingFireStickRecipeTypeDescriptor(),
            New AddFuelCookingFirePlankRecipeTypeDescriptor(),
            New AddFuelCookingFireLogRecipeTypeDescriptor(),
            New AddFuelCookingFireCharcoalRecipeTypeDescriptor(),
            New AddFuelFurnaceCharcoalRecipeTypeDescriptor()
        }.
        ToDictionary(Function(x) x.RecipeType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(recipeType As VerbType) As IRecipeType
        Return Descriptors(recipeType)
    End Function

End Module
