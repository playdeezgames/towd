Friend Class DigPondVerbTypeDescriptor
    Inherits DigVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.DigPond,
            "Dig(Clay)",
            business.LocationType.Pond,
            business.ItemType.Clay)
    End Sub
End Class
