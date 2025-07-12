Public Interface ICharacter
    Inherits IEntity(Of ICharacterType)

    ReadOnly Property Name As String
    ReadOnly Property IsAvatar As Boolean
    ReadOnly Property IsDead As Boolean

    Property CurrentLocation As ILocation
    Function CanMove(direction As String) As Boolean
    Sub Move(direction As String)

    Sub AddKnownLocation(location As ILocation)
    Function KnowsLocation(location As ILocation) As Boolean

    Sub AddMessage(ParamArray lines() As String)
    Sub AppendMessage(ParamArray lines() As String)
    ReadOnly Property HasMessages As Boolean
    Sub DismissMessage()
    ReadOnly Property CurrentMessage As String()


    ReadOnly Property HasItems As Boolean
    Sub AddItem(item As IItem)
    Sub RemoveItem(item As IItem)
    Sub ChangeItemDurability(item As IItem, delta As Integer)

    Function GetCountOfItemType(itemType As IItemType) As Integer
    Function GetItemsOfType(ItemType As IItemType) As IEnumerable(Of IItem)
    Function GetStatisticSumOfItemType(itemType As IItemType, statisticType As String) As Integer
    Sub RemoveItemOfType(itemType As IItemType)

    ReadOnly Property ItemStacks As IEnumerable(Of IItemStack)

    Function GetDoableVerbs() As IEnumerable(Of IVerbType)
    ReadOnly Property CanDoAnyVerb As Boolean

    Function HasDone(deed As IDeed) As Boolean
    Function IsAvailable(deed As IDeed) As Boolean
    Sub SetDone(deed As IDeed)

    Sub ReportChangeStatistic(statisticType As String, delta As Integer)

    Function CanAdvance(skillType As ISkillType) As Boolean

    Function StartDialog(otherCharacter As ICharacter) As IDialog
    ReadOnly Property CanDialog As Boolean
End Interface
