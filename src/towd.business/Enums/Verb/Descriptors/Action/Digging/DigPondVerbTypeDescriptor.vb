Friend Class DigPondVerbTypeDescriptor
    Inherits DigVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.DigPond,
            "Dig(Clay)",
            data.LocationType.Pond,
            business.ItemType.Clay)
    End Sub
End Class
