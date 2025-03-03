Imports System.Runtime.CompilerServices
Imports towd.data

Module RecipeTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of RecipeType, IRecipeType) =
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
            New CharcoalRecipeTypeDescriptor(),
            New CookedGrubRecipeTypeDescriptor(),
            New CookedFishFiletRecipeTypeDescriptor(),
            New UnfiredBrickRecipeTypeDescriptor(),
            New KnifeRecipeTypeDescriptor(),
            New BladeRecipeTypeDescriptor(),
            New RawFishFiletRecipeTypeDescriptor()
        }.
        ToDictionary(Function(x) x.RecipeType, Function(x) x)
    <Extension>
    Friend Function ToDescriptor(recipeType As RecipeType) As IRecipeType
        Return Descriptors(recipeType)
    End Function

End Module
