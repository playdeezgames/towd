Friend Class EatCarrotVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatCarrot,
            business.VerbCategoryType.Eat,
            ItemType.Carrot,
            "Eh, what's up, doc?",
            New Dictionary(Of Integer, Integer) From {{0, 1}})
    End Sub
End Class
