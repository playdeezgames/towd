Friend Class GrubItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Grub, "Grub", True, "A wriggling morsel from the dirt. 
Eat it raw for a quick bite, or cook it to avoid a sour gut.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
