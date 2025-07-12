Imports towd.business
Imports towd.data

Friend Class ItemTypeTopicDescriptor
    Implements ITopic
    Private Sub New(topic As String, itemType As String)
        Me.Topic = topic
        Me.ItemType = itemType
    End Sub

    Friend Shared Function Create(topic As String, enumTypeValue As String) As ITopic
        Return New ItemTypeTopicDescriptor(topic, enumTypeValue)
    End Function

    Private ReadOnly Property ItemType As String

    Public ReadOnly Property Topic As String Implements ITopic.Topic

    Public ReadOnly Property Title As String Implements ITopic.Title
        Get
            Return ItemType.ToItemTypeDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property Content As String Implements ITopic.Content
        Get
            Return ItemType.ToItemTypeDescriptor.Description
        End Get
    End Property
End Class
