Friend Class ForagePineVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.ForagePine,
            "Forage(Sticks)",
            data.LocationType.Pine,
            business.ItemType.Stick)
    End Sub
End Class
