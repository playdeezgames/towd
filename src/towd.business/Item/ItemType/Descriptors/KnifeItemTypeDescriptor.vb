Friend Class KnifeItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Knife, "Knife")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
