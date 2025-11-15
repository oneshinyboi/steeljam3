using UnityEngine;
using UnityEngine.UI;

public class FuseSliderPuzzle : BasePuzzle
{
    [System.Serializable]
    public class Fuse
    {
        public Slider slider;
        [Range(0, 10)] public int targetValue;
    }

    [Header("Fuse Setup")]
    [SerializeField] private Fuse[] fuses;

    [Header("Randomisation")]
    [SerializeField] private bool randomiseTargets = true;
    [SerializeField] private int minValue = 0;
    [SerializeField] private int maxValue = 5;

    [Tooltip("Randomise starting slider positions so they don't start solved.")]
    [SerializeField] private bool randomizeStartValues = true;

    public override void Init(PuzzleManager owningManager)
    {
        base.Init(owningManager);

        SetupFuses();
        CheckIfSolved();
    }

    private void SetupFuses()
    {
        if (fuses == null || fuses.Length == 0)
        {
            Debug.LogWarning("FuseSliderPuzzle has no fuses assigned.");
            return;
        }

        foreach (var fuse in fuses)
        {
            if (fuse.slider == null) continue;

            // Ensure sliders are using whole numbers
            fuse.slider.wholeNumbers = true;
            fuse.slider.minValue = minValue;
            fuse.slider.maxValue = maxValue;

            // Randomise target if enabled
            if (randomiseTargets)
                {
                    fuse.targetValue = Random.Range(minValue, maxValue + 1);
                }

            // Randomise starting value (try not to start already solved)
            if (randomizeStartValues)
            {
                int startVal = Random.Range(minValue, maxValue + 1);

                // If it's accidentally the same as the target and we have room, bump it
                if (startVal == fuse.targetValue && maxValue > minValue)
                {
                    startVal = (startVal == maxValue) ? startVal - 1 : startVal + 1;
                }

                fuse.slider.value = startVal;
            }
            else
            {
                // Default to min if not randomising
                fuse.slider.value = minValue;
            }

            // Listen for changes
            fuse.slider.onValueChanged.AddListener(OnFuseValueChanged);
        }
    }

    private void OnFuseValueChanged(float _)
    {
        // Any slider moved -> check if puzzle is now solved
        CheckIfSolved();
    }

    private void CheckIfSolved()
    {
        if (IsCompleted) return; // already done

        foreach (var fuse in fuses)
        {
            if (fuse.slider == null) continue;

            int current = Mathf.RoundToInt(fuse.slider.value);
            if (current != fuse.targetValue)
            {
                return; // one is wrong -> not solved yet
            }
        }

        // All fuses match their target values
        CompletePuzzle();
    }
}
