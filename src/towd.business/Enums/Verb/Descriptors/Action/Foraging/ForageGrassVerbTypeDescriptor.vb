Friend Class ForageGrassVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.ForageGrass,
            "Forage(Plant Fibers)",
            business.LocationType.Grass,
            business.ItemType.PlantFiber)
    End Sub
End Class
