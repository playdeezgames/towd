﻿Friend Class CookingFireItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.CookingFire, "Cooking Fire")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
