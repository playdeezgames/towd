Friend Class StickItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.Stick, "Stick", True, "A simple branch from the Wastes. 
Strike with it, fuel a fire, or craft something better. 
Its worth depends on your ingenuity.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
