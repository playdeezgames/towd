Imports towd.data
Imports towd.business

Friend Class SkillTypeTopicDescriptor
    Implements ITopic

    Public Sub New(topic As Topic, skillType As String)
        Me.Topic = topic
        Me.SkillType = skillType
    End Sub

    Public ReadOnly Property Topic As Topic Implements ITopic.Topic
    Private ReadOnly SkillType As String

    Public ReadOnly Property Title As String Implements ITopic.Title
        Get
            Return SkillType.ToSkillTypeDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property Content As String Implements ITopic.Content
        Get
            Return SkillType.ToSkillTypeDescriptor.Description
        End Get
    End Property
End Class
