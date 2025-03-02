Friend Class HatchetItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Hatchet, "Hatchet")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
