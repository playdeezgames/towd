Friend Class CookedFishFiletItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.CookedFishFilet, "Cooked Fish Filet")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
