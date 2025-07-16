Friend Class ForagePineVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.ForagePine,
            "Forage(Sticks)",
            business.LocationType.Pine,
            business.ItemType.Stick,
            "What's brown and sticky? A stick!")
    End Sub
End Class
