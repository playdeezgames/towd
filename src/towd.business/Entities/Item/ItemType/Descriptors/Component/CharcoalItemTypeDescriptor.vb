﻿Friend Class CharcoalItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.Charcoal, "Charcoal", True, "Burned wood residue. 
Fuel a furnace or sketch plans—its dark heart holds hidden uses.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
