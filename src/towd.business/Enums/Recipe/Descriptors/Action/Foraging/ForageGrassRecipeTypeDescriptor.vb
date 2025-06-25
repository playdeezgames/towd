Friend Class ForageGrassRecipeTypeDescriptor
    Inherits ForageRecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            RecipeType.ForageGrass,
            "Forage(Plant Fibers)",
            data.LocationType.Grass,
            data.ItemType.PlantFiber)
    End Sub
End Class
