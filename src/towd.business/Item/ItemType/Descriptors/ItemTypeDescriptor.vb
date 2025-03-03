Imports towd.data

Public MustInherit Class ItemTypeDescriptor
    Implements IItemType
    Sub New(itemType As ItemType, name As String)
        Me.ItemType = itemType
        Me.Name = name
    End Sub
    Public ReadOnly Property ItemType As ItemType Implements IItemType.ItemType
    Public ReadOnly Property Name As String Implements IItemType.Name
    Public MustOverride Sub Initialize(item As IItem) Implements IItemType.Initialize
    Public MustOverride Sub AdvanceTime(item As IItem, amount As Integer) Implements IItemType.AdvanceTime
    Public Overridable Function Describe(item As IItem) As String Implements IItemType.Describe
        Return Name
    End Function
End Class
