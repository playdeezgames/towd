﻿Friend Class UnfiredBrickItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.UnfiredBrick, "Unfired Brick")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
