package Agent;

import javafx.scene.control.TextArea;
import java.util.ArrayList;
import java.util.Random;

public class TrapSender implements Runnable{

    private ArrayList<RSUAgent> rsuAgents;
    private javafx.scene.control.TextArea log;
    private int periodicity;

    public void setMaxVariance(int maxVariance) {
        if(Math.abs(maxVariance) > periodicity)
            this.maxVariance = periodicity-1;
        else
            this.maxVariance = maxVariance;
    }

    public void setMinVarinace(int minVarinace) {
        if(Math.abs(minVarinace) > periodicity)
            this.minVarinace = Math.negateExact(periodicity) + 1;
        else
            this.minVarinace = minVarinace;
    }

    private int maxVariance;
    private int minVarinace;
    private volatile boolean running;

    public TrapSender(ArrayList<RSUAgent> rsuAgents, TextArea log, int periodicity, int minVarinace, int maxVariance)
    {
        this.rsuAgents = rsuAgents;
        this.log = log;
        this.periodicity = periodicity;
        this.minVarinace = minVarinace;
        this.maxVariance = maxVariance;
        running = true;
    }

    public void setPeriodicity(int periodicity)
    {
        this.periodicity = periodicity;
    }

    @Override
    public void run() {
        Random rnd = new Random();
        while(running)
        {
            try
            {
                for (RSUAgent rsu : rsuAgents)
                {
                    if (rsu.SendTrap)
                    {
                        rsu.agent.sendTrap();
                        log.appendText("Trap sent: " + rsu.IP + "/" + rsu.Port + " --> " + rsu.TrapAddress + "\n");
                        Thread.sleep(periodicity + (rnd.nextInt((maxVariance-minVarinace)+1) + minVarinace));
                    }
                }

                //Thread.sleep(periodicity);
            }
            catch (InterruptedException e)
            {
                e.printStackTrace();
            }

        }
    }

    public void shutdown()
    {
        running = false;
    }
}