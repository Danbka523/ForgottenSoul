using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator; //

    [SerializeField] private Transform target;
    [SerializeField] private float View_Angle;// ���� ������ ����������
    [SerializeField] private float nrm_distance;// ��������� ����� ���������� � ����������� , ����� ���� ��������� ������������
    private float angular_speed;// �������� ��������
    private NavMeshAgent agent;
    private Transform agentTransform;
    private float distance_trigger = 1.0f;// ���� ���������� �������� �� ���� ��������� 
    private float distance_stop = 3.0f;//���������� �� ���������, ������� ����� �� ����� ���������
    private Rigidbody rig_enemy;

    void Start()
    {
        animator = GetComponent<Animator>(); //

        agent = GetComponent<NavMeshAgent>();
        angular_speed = agent.angularSpeed;
        agentTransform = agent.transform;
        agent.updateRotation = false;
        rig_enemy = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (In_sight() || Vector3.Distance(transform.position, target.position) <= distance_trigger)
        {
            

            MoveToTarget(); 
        }
        else animator.SetFloat("Move", 0); //
    }


    bool In_sight()
    {
        float angle_target = Vector3.Angle(transform.forward, target.position - transform.position);
        if (Mathf.Abs(angle_target) <= View_Angle && Vector3.Distance(transform.position, target.position) <= nrm_distance)
        {
            return true;
        }
        rig_enemy.velocity = Vector3.zero;
        rig_enemy.angularVelocity = Vector3.zero; // ����� ��� ����, ����� ������� �� ���������  � �� ��������� � ��������� �����
        return false;

    }

    private void MoveToTarget()
    {
        animator.SetFloat("Move", 1); //
        RotateToTarget();
        agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) <= distance_stop)
        {

            agent.isStopped = true;
            rig_enemy.velocity = Vector3.zero;
            rig_enemy.angularVelocity = Vector3.zero;

        }
        else agent.isStopped = false;

    }
    private void RotateToTarget()
    {
        Vector3 dis = (target.position - transform.position).normalized;
        dis.y = 0;//����� ��� ����, ����� �� �������� ������ ��� X ����, ����� �������� �� �������������
        if (dis == Vector3.zero)
        {
            return;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dis), angular_speed);
    }

}
