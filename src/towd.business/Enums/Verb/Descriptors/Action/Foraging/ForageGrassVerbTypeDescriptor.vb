Friend Class ForageGrassVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.ForageGrass,
            "Forage(Plant Fibers)",
            data.LocationType.Grass,
            data.ItemType.PlantFiber)
    End Sub
End Class
