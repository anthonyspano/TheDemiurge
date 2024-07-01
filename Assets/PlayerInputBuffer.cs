using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static com.ultimate2d.combat.PlayerController;

// test class that uses a list for the buffer
namespace com.ultimate2d.combat
{
    public class InputBufferMemory
    {
        public int frame;
        public PlayerController.PlayerStatus action;

        public InputBufferMemory(int i, PlayerController.PlayerStatus a)
        {
            frame = i;
            action = a;
        }
    }

    // record player inputs into a buffer and execute the nearest one available
    public class PlayerInputBuffer : MonoBehaviour
    {
        private static PlayerInputBuffer _instance;
        public static PlayerInputBuffer Instance
        {
            get { return _instance; }
        }
        
        public List<InputBufferMemory> InputBuffer;
        public int bufferSize; // 150

        public int index = 0; 

        public int InputBufferWindow; // how far to look back in the input buffer

        void Awake()
        {
            // singleton
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        void Start()
        {
            InputBuffer = new List<InputBufferMemory>();

            for(int i = bufferSize; i > 0; i--)
            {
                InputBuffer.Add(new InputBufferMemory(0, PlayerController.PlayerStatus.Neutral));
            }
        }

        void FixedUpdate()
        {
            Add(new InputBufferMemory(Time.frameCount, PlayerController.PlayerStatus.Neutral));

        }

        public void Add(InputBufferMemory ibm)
        {
            index++;
            index = index % bufferSize;
            InputBuffer[index] = ibm;

        }

        public PlayerStatus GetCommand()
        {
            return GetCommand(InputBufferWindow);
        }

        public PlayerStatus GetCommand(int framesToRead) // did I do this input in the last 100 frames?
        {
            int reader = index;

            if(framesToRead >= InputBuffer.Count)
                framesToRead = InputBuffer.Count;

            for(int i = framesToRead; i > 0; i--)
            {
                if(InputBuffer[reader].action != PlayerController.PlayerStatus.Neutral)
                    return InputBuffer[reader].action;

                reader--;
                if(reader < 0) reader = InputBuffer.Count - 1;
            }

            return PlayerController.PlayerStatus.Neutral;
        }


    }
}