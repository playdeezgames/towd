Imports System.Runtime.CompilerServices
Imports towd.data

Public Module RecipeTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of RecipeType, IRecipeType) =
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
            New WaitRecipeTypeDescriptor()
        }.
        ToDictionary(Function(x) x.RecipeType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(recipeType As RecipeType) As IRecipeType
        Return Descriptors(recipeType)
    End Function

End Module
