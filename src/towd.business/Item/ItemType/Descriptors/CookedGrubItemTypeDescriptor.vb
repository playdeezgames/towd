﻿Friend Class CookedGrubItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.CookedGrub, "Cooked Grub")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
