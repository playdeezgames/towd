Friend Class FurnaceItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Furnace, "Furnace")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
