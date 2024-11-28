using System;
[System.Serializable]

public class TeamGameData
{
    public int team_id;                                 // (PK) 고유 팀 ID
    public string team_name;                    // Blue/Red Team
    public int[] members = new int[3];      // 팀 구성원 정보(3명의 user_id 리스트)
    public string team_score;                    // 팀 점수(승/패)
    public DateTime created_at;               // 팀 생성일



    public void Reset()
    {
        team_id = 0;
        team_name = string.Empty;
        members = new int[3] { 0, 0, 0 };
        team_score = string.Empty;
        created_at = DateTime.Now;
    }

}
