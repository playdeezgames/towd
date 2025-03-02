Friend Class LogItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Log, "Log")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
