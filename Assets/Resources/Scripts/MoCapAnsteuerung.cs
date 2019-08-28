using System;
using UnityEngine;
using Ventuz.OSC;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

public class MoCapAnsteuerung : MonoBehaviour
{
    public static Matrix4x4[] matrix = new Matrix4x4[21]; //Knochenmatrizen aus MoCap abspeichern
    private Transform[] bones;
    private GameObject[] sphere; //Sphären um Knochenpositionen anzuzeigen
    private static UdpReader reader;
    private OscBundle bundle;
    //Handmesh bei Fernglas an und Ausschalten
    public SkinnedMeshRenderer[] hands;
    public GameManager manager;
    private bool mocap, mocapcontrolling;
    private float[] skeleton;
    public float handabstand, handlinks, handrechts = 0;
    public Transform _00UpperChest;
    public Transform _01MidTorso;
    public Transform _02LowerTorso;
    public Transform _03LeftUpperLeg;
    public Transform _04LeftLowerLeg;
    public Transform _05LeftFoot;
    public Transform _06RightUpperLeg;
    public Transform _07RightLowerLeg;
    public Transform _08RightFoot;
    public Transform _09Neck;
    public Transform _10Head;
    public Transform _11RightClavicle;
    public Transform _12RightShoulder;
    public Transform _13RightUpperArm;
    public Transform _14RightLowerArm;
    public Transform _15RightHand;
    public Transform _16LeftClavicle;
    public Transform _17LeftShoulder;
    public Transform _18LeftUpperArm;
    public Transform _19LeftLowerArm;
    public Transform _20LeftHand;
    public float maxFernglas = 0.3f;
    public float maxclap = 0.3f;
    public static bool glas;
    private Vector3 initPosition = Vector3.zero;
    private Transform AvatarRoot;
    private int posenumber = 0, previouspose = 0;

    private GameObject hat, knife1, knife2;
    bool jumpEnabled = true;

    public void Awake()
    {
        bones = new Transform[21];

        bones[0] = _00UpperChest;
        bones[1] = _01MidTorso;
        bones[2] = _02LowerTorso;
        bones[3] = _03LeftUpperLeg;
        bones[4] = _04LeftLowerLeg;
        bones[5] = _05LeftFoot;
        bones[6] = _06RightUpperLeg;
        bones[7] = _07RightLowerLeg;
        bones[8] = _08RightFoot;
        bones[9] = _09Neck;
        bones[10] = _10Head;
        bones[11] = _11RightClavicle;
        bones[12] = _12RightShoulder;
        bones[13] = _13RightUpperArm;
        bones[14] = _14RightLowerArm;
        bones[15] = _15RightHand;
        bones[16] = _16LeftClavicle;
        bones[17] = _17LeftShoulder;
        bones[18] = _18LeftUpperArm;
        bones[19] = _19LeftLowerArm;
        bones[20] = _20LeftHand;
    }

    void Start()
    {
        if (reader == null)
            reader = new UdpReader(4000);
        sphere = new GameObject[21];
        skeleton = new float[336];
        AvatarRoot = GameObject.FindGameObjectWithTag("AvatarRoot").transform;
        for (int i = 0; i < 21; i++)
        {   //hands and feet and head
            if (i == 5 || i == 8 || i == 10 || i == 15 || i == 20)
            {
                sphere[i] = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/SphereCollider"), new Vector3(0f, 0f, 0f), Quaternion.identity); //Sphären Instanzieren und Erstellen
            }
            if (i == 15)
            {
                knife1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Knife_01 (1)"), sphere[15].transform.localPosition, Quaternion.identity); // spawn knife
            }
            if (i == 20)
            {
                knife2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Knife_01 (1)"), sphere[20].transform.localPosition, Quaternion.identity); // spawn knife
            }
            else
                sphere[i] = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Sphere"), new Vector3(0f, 0f, 0f), Quaternion.identity); //Sphären Instanzieren und Erstellen
            sphere[i].transform.parent = AvatarRoot;
            if (i == 10)
            {
                hat = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/hat Variant"), sphere[10].transform.localPosition, Quaternion.identity); // spawn hat
            }

            //Debug.Log(skeleton[10]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*	BoneInfos
		Index	Bone Name		Position in Hierarchy
		0		Upper Chest 	Root
		1		Mid Torso		Child of Upper Chest
		2		Lower Torso		Child of Mid Torso
		3		Left Upper Leg	Child of Lower Torso
		4		Left Lower Leg	Child of Left Upper Leg
		5		Left Foot		Child of Left Lower Leg
		6		Right Upper Leg	Child of Lower Torso
		7		Right Lower Leg	Child of Right Upper Leg
		8		Right Foot 		Child of Right Lower Leg
		9		Neck			Child of Upper Chest
		10		Head 			Child of Neck
		11		Right Clavicle	Child of Upper Chest
		12		Right Shoulder	Child of Clavicle
		13		Right Upper Arm	Child of Shoulder
		14		Right Lower Arm	Child of Upper Arm
		15		Right Hand		Child of Lower Arm
		16		Left Clavicle	Child of Upper Chest
		17		Left Shoulder	Child of Left Clavicle
		18		Left Upper Arm	Child of Left Shoulder
		19		Left Lower Arm	Child of Left Upper Arm
		20		Left Hand		Child of Left Lower Arm
		*/

        #region MoCap Ansteuerung
        if (reader != null)
            bundle = (OscBundle)reader.Receive(); //Aktuelle MoCap Position empfangen

        bundle = null; //for testing not connected environment

        if (bundle != null)
        {
            int i = 0;
            foreach (OscElement m in bundle.Elements)
            {
                skeleton[i] = (float)m.Args[0];
                i++;
            }
            //print("Recieved Bundle");

            //Save for once on pressed button
            if (Input.GetKeyDown(KeyCode.F1))
            {
                print("Saving F1");
                saveSkeleton("pose1");
            }

            #region Saving Multiple Positions

            if (Input.GetKeyDown(KeyCode.F2))
            {
                saveSkeleton("pose2");
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                saveSkeleton("pose3");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                saveSkeleton("pose4");
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                saveSkeleton("pose5");
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                saveSkeleton("pose6");
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                saveSkeleton("pose7");
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                saveSkeleton("pose8");
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                saveSkeleton("pose9");
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                saveSkeleton("pose10");
            }

            #endregion

            mocap = true;
            mocapcontrolling = false;
        }
        else
        {
            //Load for once if no bundle found
            //if successfull, set mocap true, else quit

            if (posenumber == 0) //no pose has been set yet
            {
                mocap = loadSkeleton(skeleton, "pose6");
                posenumber = 6;
            }

            #region LoadPoses

            if (Input.GetKeyDown(KeyCode.Alpha1) && posenumber != 1)
            {
                mocap = loadSkeleton(skeleton, "pose1");
                posenumber = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && posenumber != 2)
            {
                mocap = loadSkeleton(skeleton, "pose2");
                posenumber = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && posenumber != 3)
            {
                mocap = loadSkeleton(skeleton, "pose3");
                posenumber = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && posenumber != 4)
            {
                mocap = loadSkeleton(skeleton, "pose4");
                posenumber = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && posenumber != 5)
            {
                mocap = loadSkeleton(skeleton, "pose5");
                posenumber = 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && posenumber != 6)
            {
                mocap = loadSkeleton(skeleton, "pose6");
                posenumber = 6;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && posenumber != 7)
            {
                mocap = loadSkeleton(skeleton, "pose7");
                posenumber = 7;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && posenumber != 8)
            {
                mocap = loadSkeleton(skeleton, "pose8");
                posenumber = 8;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) && posenumber != 9)
            {
                mocap = loadSkeleton(skeleton, "pose9");
                posenumber = 9;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0) && posenumber != 10)
            {
                mocap = loadSkeleton(skeleton, "pose10");
                posenumber = 10;
            }
            #endregion

            mocapcontrolling = true;

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (posenumber != 7)
                {
                    previouspose = posenumber;
                    loadSkeleton(skeleton, "pose7");
                    posenumber = 7;
                }
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                mocap = loadSkeleton(skeleton, "pose" + previouspose);
                posenumber = previouspose;
            }

            // set hat to specific position and rotation
            Vector3 pos = new Vector3(sphere[10].transform.position.x, sphere[10].transform.position.y - 0.18f, sphere[10].transform.position.z);
            hat.transform.position = pos;
            hat.transform.rotation = sphere[9].transform.localRotation;

            // set knifes to specific position and rotation
            Vector3 pos1 = new Vector3(sphere[15].transform.position.x, sphere[15].transform.position.y, sphere[15].transform.position.z);
            knife1.transform.position = pos1;
            knife1.transform.rotation = sphere[15].transform.localRotation;
            Vector3 pos2 = new Vector3(sphere[20].transform.position.x, sphere[20].transform.position.y, sphere[20].transform.position.z);
            knife2.transform.position = pos2;
            knife2.transform.rotation = sphere[20].transform.localRotation;
        }


        if (mocap)
        {

            //Speichern in Matrizen zur Weiterverarbeitung
            int x = 0;
            for (int anz = 0; anz < 21; anz++)
            {
                matrix[anz].m00 = skeleton[x++];
                matrix[anz].m01 = skeleton[x++];
                matrix[anz].m02 = skeleton[x++];
                matrix[anz].m03 = skeleton[x++];
                matrix[anz].m10 = skeleton[x++];
                matrix[anz].m11 = skeleton[x++];
                matrix[anz].m12 = skeleton[x++];
                matrix[anz].m13 = skeleton[x++];
                matrix[anz].m20 = skeleton[x++];
                matrix[anz].m21 = skeleton[x++];
                matrix[anz].m22 = skeleton[x++];
                matrix[anz].m23 = skeleton[x++];
                matrix[anz].m30 = skeleton[x++];
                matrix[anz].m31 = skeleton[x++];
                matrix[anz].m32 = skeleton[x++];
                matrix[anz].m33 = skeleton[x++];
            }






            for (int anz = 0; anz < 21; anz++)
            {
                //Rotation nicht für den Kopf
                if (anz != 10)
                    bones[anz].transform.localRotation = MoCapUtils.GetRotation(matrix[anz]);
                sphere[anz].transform.localPosition = MoCapUtils.GetPosition(matrix[anz]);

            }




            //Manual Controlling
            if (mocapcontrolling)
            {
                if (Input.GetKey(KeyCode.C))
                {
                    AvatarRoot.transform.Translate(new Vector3(Input.GetAxis("Vertical") * -0.03f, 0, Input.GetAxis("Horizontal") * 0.03f));
                }
                else
                {
                    AvatarRoot.transform.Translate(new Vector3(Input.GetAxis("Vertical") * -0.06f, 0, Input.GetAxis("Horizontal") * 0.06f));
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    AvatarRoot.Rotate(new Vector3(0, 1, 0), -5f);
                }
                if (Input.GetKey(KeyCode.E))
                {
                    AvatarRoot.Rotate(new Vector3(0, 1, 0), 5f);
                }

                //Jump on space
                if (Input.GetKeyDown(KeyCode.Space) && jumpEnabled)
                {
                    // Vector3 pos = new Vector3(AvatarRoot.transform.position.x, AvatarRoot.transform.position.y + 1, AvatarRoot.transform.position.z);
                    StartCoroutine(enableJumping());
                    //AvatarRoot.transform.Translate(new Vector3(Input.GetAxis("Vertical") * 0.06f, 0, Input.GetAxis("Horizontal") * 0.03f));
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");
                    //  float moveForward = Input.GetAxis("Forward");

                    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

                    //  Vector3 jump = new Vector3(0.0f, moveForward, 0.0f);

                    Rigidbody rb = (Rigidbody)AvatarRoot.GetComponent<Rigidbody>();
                    //rb.AddForce(movement * jump);

                    rb.velocity = new Vector3(0, 4f, 0);
                }


                #region LimittoPlayarea
                float avatarx, avatarz, avatary;
                avatary = AvatarRoot.position.y;
                if (AvatarRoot.position.z > 2.5f)
                {
                    avatarz = 2.5f;
                }
                else
                {
                    if (AvatarRoot.position.z < -2f)
                    {
                        avatarz = -2f;
                    }
                    else
                    {
                        avatarz = AvatarRoot.position.z;
                    }
                }
                if (AvatarRoot.position.x > 1.4f)
                {
                    avatarx = 1.4f;
                }
                else
                {
                    if (AvatarRoot.position.x < -1.8f)
                    {
                        avatarx = -1.8f;
                    }
                    else
                    {
                        avatarx = AvatarRoot.position.x;
                    }
                }


                //AvatarRoot.transform.position = new Vector3(avatarx, 0, avatarz);
                AvatarRoot.transform.position = new Vector3(avatarx, avatary, avatarz);
                #endregion 
            }
            else //controlling via MoCap, locking manual controlling
            {
                AvatarRoot.transform.position = new Vector3(0, 0, 0);


                if (initPosition == Vector3.zero) initPosition = sphere[1].transform.localPosition;
                //Rotation im Stillstand
                if (sphere[1].transform.localPosition.x > initPosition.x - 0.01f && sphere[1].transform.localPosition.x < initPosition.x + 0.01f &&
                    sphere[1].transform.localPosition.y > initPosition.y - 0.01f && sphere[1].transform.localPosition.y < initPosition.y + 0.01f &&
                    sphere[1].transform.localPosition.z > initPosition.z - 0.01f && sphere[1].transform.localPosition.z < initPosition.z + 0.01f)
                {
                    //AvatarRoot.rotation = Quaternion.Euler(0f,90f,0f);
                    //Debug.Log("Still");
                }
                else
                {
                    AvatarRoot.rotation = Quaternion.Euler(0f, 0f, 0f);
                    //Debug.Log ("Bewegt");
                }
            }

            /*
			//Head and Neck
			bones [9].Rotate (180, 180, 0);
			//bones[10].Rotate(180,180,0);
			//left Arm
			for (int i=16; i<=20; i++)
				bones [i].Rotate (0, 90, 90);
			//right Arm
			for (int i=11; i<=15; i++)
				bones [i].Rotate (0, -90, -90);
			//Foots
			bones [5].Rotate (90, 0, 0);
			bones [8].Rotate (90, 0, 0);



	

			//Fernglastest
			//9= Nacken 10 = Kopf 15=Handrechts 20=Handlinks
			if (Math.Abs ((MoCapUtils.GetPosition (matrix [10]) - MoCapUtils.GetPosition (matrix [15])).magnitude) < maxFernglas && 
                Math.Abs ((MoCapUtils.GetPosition (matrix [10]) - MoCapUtils.GetPosition (matrix [20])).magnitude) < maxFernglas && 
                Math.Abs ((MoCapUtils.GetPosition (matrix [15]) - MoCapUtils.GetPosition (matrix [20])).magnitude) < maxFernglas) {
				glas = true; //Fernglas Aktiv
				for (int i = 0; i < hands.Length; i++)
					hands [i].enabled = false;
			} else {
				glas = false; //Fernglas nicht aktiv
				for (int i = 0; i < hands.Length; i++)
					hands [i].enabled = true;
				
			}
            */


        }
        #endregion
    }

    public IEnumerator enableJumping()
    {
        jumpEnabled = false;
        yield return new WaitForSecondsRealtime(1.1f);
        jumpEnabled = true;
    }

    //Allowing to Save/Load Avatars Base Position from MoCap to OfflineVersion
    public void saveSkeleton(string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/" + filename + ".dat");

        AvatarData data = new AvatarData();
        data.skeleton = new float[336];

        int length = this.skeleton.Length;
        for (int i = 0; i < length; i++)
        {
            data.skeleton[i] = this.skeleton[i];
        }

        bf.Serialize(file, data);
        file.Close();
        print("File " + filename + " has been saved");
    }

    public bool loadSkeleton(float[] skeletonarray, String filename)
    {
        if (File.Exists(Application.dataPath + "/" + filename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/" + filename + ".dat", FileMode.Open);
            AvatarData data = (AvatarData)bf.Deserialize(file);
            file.Close();

            int length = data.skeleton.Length;
            for (int i = 0; i < length; i++)
            {
                skeletonarray[i] = data.skeleton[i];
            }

            return true;
        }
        return false;
    }
}

//Allowing to Save/Load Avatars Base Position from MoCap to OfflineVersion

[Serializable]
class AvatarData
{
    public float[] skeleton;
}