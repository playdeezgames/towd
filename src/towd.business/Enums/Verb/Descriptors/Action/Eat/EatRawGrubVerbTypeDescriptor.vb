Friend Class EatRawGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatRawGrub,
            business.VerbCategoryType.Eat,
            ItemType.Grub,
            "Is this 'Fear Factor'? Is Joe Rogan somewhere around here?",
            New Dictionary(Of Integer, Integer) From {{0, 1}, {1, 1}})
    End Sub
End Class
