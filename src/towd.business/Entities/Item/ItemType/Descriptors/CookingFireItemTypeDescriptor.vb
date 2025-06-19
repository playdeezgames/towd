Friend Class CookingFireItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.CookingFire, "Cooking Fire", True, "A flickering camp blaze. 
Cook food or warm yourself—tend it well, or it dies with you.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
