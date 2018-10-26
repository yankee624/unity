using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalButton : MonoBehaviour {

    public string animalName;

    public int moneyAmount;
    // moneySpawnTime이 defaultMoneySpawnTime에서 시작해서 점점 줄어들어서 0되면 money 나옴.
    public float moneySpawnTime;
    public float defaultMoneySpawnTime;

    private Rigidbody2D rb;
    private Animator anim;
    private Canvas canvas;

    public GameObject money;
    public bool canSpawnMoney = true;

    public GameObject poo;
    public bool canSpawnPoo = true;

    public bool rest = false;

    public Vector2 dir;

    private Coroutine moveCoroutine;
    private Coroutine collideAnimalCoroutine;
    private Coroutine collideWallCoroutine;

    public void OnMouseDown()
    {
        moneySpawnTime -= 0.8f;
        anim.SetTrigger("Clicked");
    }


    IEnumerator MakeMoney()
    {
        canSpawnMoney = false;

        while(moneySpawnTime >= 0)
        {
            moneySpawnTime -= Time.deltaTime;
            yield return null;
        }

        GameObject newMoney = Instantiate(money) as GameObject;

        newMoney.transform.SetParent(canvas.transform, false);
        newMoney.transform.localPosition = gameObject.transform.localPosition;
        newMoney.GetComponent<Money>().animalButton = this;
        //newMoney.transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator MakePoo()
    {
        canSpawnPoo = false;
        float randomTime = Random.Range(5f, 30f);
        yield return new WaitForSeconds(randomTime);
        GameObject newPoo = Instantiate(poo) as GameObject;

        newPoo.transform.SetParent(canvas.transform, false);
        newPoo.transform.localPosition = gameObject.transform.localPosition;

        canSpawnPoo = true;
    }

    public float speed = 5f;
    // Real Rigidbdoy 사용할 경우
    IEnumerator MoveAround()
    {
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        while (true)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            //if (randomDir.x >= 0)
            //{
            //    gameObject.transform.localEulerAngles = new Vector3(0, 120, 0);
            //}
            //else
            //{
            //    gameObject.transform.localEulerAngles = new Vector3(0, 240, 0);
            //}
            float angle = Mathf.Atan2(randomDir.y, randomDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);


            anim.SetTrigger("Walk");

            rb.AddForce(randomDir*2, ForceMode2D.Impulse);
            yield return new WaitUntil(() => rb.velocity.magnitude < 0.3f);

            rb.velocity = Vector2.zero;
            anim.SetTrigger("Idle A");
            yield return new WaitForSeconds(1.5f);
        }
    }

    // kinematic rigidbody 사용할 경우

    //IEnumerator MoveAround()
    //{
    //    yield return new WaitForSeconds(Random.Range(0f, 2f));
    //    while (true)
    //    {
    //        dir = Random.insideUnitCircle.normalized;
    //        if (dir.x <= 0)
    //        {
    //            gameObject.transform.localEulerAngles = new Vector3(0, 120, 0);
    //        }
    //        else
    //        {
    //            gameObject.transform.localEulerAngles = new Vector3(0, 240, 0);
    //        }

    //        anim.SetTrigger("Walk");
    //        for (int i = 0; i < 20; i++)
    //        {
    //            transform.Translate(dir * 0.1f);
    //            yield return new WaitForSeconds(0.05f);
    //        }

    //        anim.SetTrigger("Idle A");
    //        yield return new WaitForSeconds(1f);
    //    }
    //}


    private void Start()
    {
        // 이 스크립트가 부착되어있는 game object의 다른 component를 가져올땐 GetComponent씀
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        moneySpawnTime = defaultMoneySpawnTime;

        moveCoroutine = StartCoroutine(MoveAround());
    }

    private void Update()
    {
        if (!rest && canSpawnMoney)
        {
            StartCoroutine(MakeMoney());
        }
        if (canSpawnPoo)
        {
            StartCoroutine(MakePoo());
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Animal")
        {
            if (collideAnimalCoroutine != null)
                StopCoroutine(collideAnimalCoroutine);
            collideAnimalCoroutine = StartCoroutine(CollideAnimal());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            //Debug.Log("wall collided");
            //if (collideWallCoroutine != null)
            //    StopCoroutine(collideWallCoroutine);
            //collideWallCoroutine = StartCoroutine(CollideWall(collision));
            
        }
    }





    IEnumerator CollideAnimal()
    {
        //if(moveCoroutine!=null)
        //    StopCoroutine(moveCoroutine);
        //if(collideWallCoroutine != null)
        //    StopCoroutine(collideWallCoroutine);
        //anim.SetTrigger("Bounce");

        //yield return new WaitForSeconds(2f);

        //moveCoroutine = StartCoroutine(MoveAround());

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        rb.velocity = Vector2.zero;
        anim.SetTrigger("Bounce");
        yield return new WaitForSeconds(2f);
        moveCoroutine = StartCoroutine(MoveAround());


    }

    IEnumerator CollideWall(Collider2D collision)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        if (collideAnimalCoroutine != null)
            StopCoroutine(collideAnimalCoroutine);
        dir = -dir;

        anim.SetTrigger("Walk");
        for (int i = 0; i < 5; i++)
        {
            transform.Translate(dir * 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        anim.SetTrigger("Idle A");

        moveCoroutine = StartCoroutine(MoveAround());
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Animal")
    //    {
    //        anim.SetTrigger("Bounce");
    //        collided = true;
    //        collision.gameObject.GetComponent<Animator>().SetTrigger("Bounce");
    //        collision.gameObject.GetComponent<AnimalButton>().collided = true;

    //        Debug.Log(animalName);
    //        Debug.Log(collision.gameObject.GetComponent<AnimalButton>().animalName);
    //    }
    //}

}
