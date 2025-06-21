Imports towd.data

Public Interface ISkillType
    ReadOnly Property SkillType As SkillType
    ReadOnly Property Name As String
    ReadOnly Property Description As String
    ReadOnly Property StatisticType As StatisticType
    Function CanAdvance(character As ICharacter) As Boolean
    Function Advance(character As ICharacter) As Boolean
    Function GetDescription(character As ICharacter) As String
End Interface
