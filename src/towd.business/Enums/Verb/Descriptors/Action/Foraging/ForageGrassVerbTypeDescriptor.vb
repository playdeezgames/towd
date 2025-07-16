Friend Class ForageGrassVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.ForageGrass,
            "Forage(Plant Fibers)",
            business.LocationType.Grass,
            business.ItemType.PlantFiber,
            "Have you tried increasing yer fiber intake?")
    End Sub
End Class
