Friend Class ForagePineVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.ForagePine,
            "Forage(Sticks)",
            business.LocationType.Pine,
            business.ItemType.Stick)
    End Sub
End Class
