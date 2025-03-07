﻿Imports towd.data

Public MustInherit Class SkillTypeDescriptor
    Implements ISkillType

    Sub New(skillTYpe As SkillType, name As String, statisticType As StatisticType)
        Me.SkillType = skillTYpe
        Me.Name = name
        Me.StatisticType = statisticType
    End Sub

    Public ReadOnly Property SkillType As SkillType Implements ISkillType.SkillType

    Public ReadOnly Property Name As String Implements ISkillType.Name

    Public ReadOnly Property StatisticType As StatisticType Implements ISkillType.StatisticType
    Public MustOverride Function CanAdvance(character As ICharacter) As Boolean Implements ISkillType.CanAdvance
    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
