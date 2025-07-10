Friend Class SharpRockItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.SharpRock, "Sharp Rock", True, "A honed stone edge. 
Cut, scrape, or stab with care—its sharpness fades, so use it wisely.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
