Imports towd.data

Public Interface IItemType
    ReadOnly Property ItemType As ItemType
    ReadOnly Property Name As String
    Sub Initialize(item As IItem)
    Sub AdvanceTime(item As IItem, amount As Integer)
    Function Describe(item As IItem) As String
    ReadOnly Property IsAggregate As Boolean
    ReadOnly Property Description As String
End Interface
