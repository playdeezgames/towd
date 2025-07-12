Friend Class EatCarrotVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatCarrot, business.VerbCategoryType.Eat, ItemType.Carrot)
    End Sub
End Class
