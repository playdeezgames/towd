Friend Class DigPondVerbTypeDescriptor
    Inherits DigVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.DigPond,
            "Dig(Clay)",
            business.LocationType.Pond,
            business.ItemType.Clay,
            "For those who aspire to be potters... hairy potters.")
    End Sub
End Class
