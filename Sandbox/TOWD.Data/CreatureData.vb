Public Class CreatureData
    Public Property CreatureType As CreatureType
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property OnInteract As Integer?
End Class
