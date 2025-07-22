Friend Class EatCarrotVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatCarrot,
            business.VerbCategoryType.Eat,
            ItemType.Carrot,
            "Eh, what's up, doc?")
    End Sub
End Class
