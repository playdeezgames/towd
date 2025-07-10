Imports towd.data

Public MustInherit Class ItemTypeDescriptor
    Implements IItemType
    Sub New(
           itemType As String,
           name As String,
           isAggregate As Boolean,
           description As String,
           Optional statistics As IReadOnlyDictionary(Of String, Integer) = Nothing)
        Me.ItemType = itemType
        Me.Name = name
        Me.IsAggregate = isAggregate
        Me.Description = description
        Me.Statistics = If(statistics, New Dictionary(Of String, Integer))
    End Sub
    Public ReadOnly Property ItemType As String Implements IItemType.ItemType
    Public ReadOnly Property Name As String Implements IItemType.Name
    Public ReadOnly Property IsAggregate As Boolean Implements IItemType.IsAggregate
    Public ReadOnly Property Description As String Implements IItemType.Description
    Public ReadOnly Property Statistics As IReadOnlyDictionary(Of String, Integer) Implements IItemType.Statistics
    Public MustOverride Sub Initialize(item As IItem) Implements IItemType.Initialize
    Public MustOverride Sub AdvanceTime(item As IItem, amount As Integer) Implements IItemType.AdvanceTime
    Public Overridable Function Describe(item As IItem) As String Implements IItemType.Describe
        Return Name
    End Function
End Class
