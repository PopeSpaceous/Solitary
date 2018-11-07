# Setting up the environment
## Part 1: Setting up Unity
### Step 1: Go to the [Unity Downloads page](https://unity3d.com/get-unity/download/archive)  

### Step 2: Under the Unity 2017.x tab navigate to the Unity 2017.3.0 Release  
<img width="350" src='Readme Assets/unity under 2017.png'> 

### Step 3: Download the release applicable to your environment  
<img width="350" src='Readme Assets/unity 3.0.png'> 

### Step 4: Launch the installer and install the program (no changes to the default installation)  
### Step 5: Log into Unity/Create an account with Unity  
<img width="350" src='Readme Assets/Log in.png'>   

### Step 5.5: If you have created an account, Unity will ask you what you are going to be using Unity for. In our case, we are using Unity in a Personal environment (Personal Edition) and not in any professional capacity.  

Once set up we can go to the next part  

## Part 2: Setting up the Project

### Step 1: Log into Github/Create an account with Github  

### Step 2: Fork the project (repository)

The first step is to create a fork of this repo.  
Do so by clicking on the fork button on the top of this page. A fork is basically your own working copy of this repository.

<img width="500" src='Readme Assets/Fork Repo.png'>   

### Step 3: Clone the project (repository)  

The next step is to clone the forked repo to your machine.

Go to your GitHub repositories and open the forked repository called Rebus (_forked from PopeSpaceous/Solitary_). Click on the "Clone or download" button and then click the copy to clipboard icon to get your url.

Finally run the following git command in your terminal:

```sh
git clone "the copied url"
```

For example:

```
git clone https://github.com/username/Solitary.git
```
### Step 4: Register an upstream Repository  
You have now created a local clone on you computer. This clone will point to your forked repository. It's also useful to have
the upstream repository (the source that you forked) registered as well to be able to stay up to date with the latest changes.

If you haven't already, start by changing your directory to the rebus repository that was created when you ran `git clone`:

```sh
cd Solitary
```

Then add `Popespacious/Solitary` as the upstream remote:

```
git remote add upstream https://github.com/Popespacious/Solitary.git
```  
### Step 5: Create a Branch  
It's common practice to create a new branch for each new feature or bugfix you are working on. Let's go ahead and create one!

First, lets make sure we have the latest version of the upstream repository by running (do this before each time you create a new branch):

```sh
git fetch upstream
```

Create your new branch by running:

```sh
git checkout -b <your-new-branch-name> upstream/master
```

> Note: Replace `<your-new-branch-name>` with something that describes the changes you are about to make

For example:

```sh
git checkout -b add-new-asset upstream/master
```

> Note: By specifying `upstream/master` we're saying that our new branch should be created from the latest upstream version  

# Part 3: Running the Game
### Step 1: Opening the Project using Unity  
If this is your first time opening unity, the ability to open a project is not avaliable to you, create a new project with default values.  

<img width="350" src='Readme Assets/Click on New.png'>  

After you enter the development screen go to the top right corner and click on file.  
<img width="350" src='Readme Assets/under file choose open project.png'>    

Click onto the open project button and navigate to where you have the project downloaded.  

Once the project is open, go to the bottom left of the window, under screens click onto MainMenu.  

<img width="350" src='Readme Assets/click on mainmenu.png'>   

In the top middle of the screen press the play button.  

<img width="350" src='Readme Assets/click on play.png'>    

Ensure that the game is running.  

### Step 2: Contribution  

After you ensure that the game is running you are able to contribute to the project in any way you see fit.  

Remember to consult the contribution documentation on how to push your changes to the main project.  

[Contributions](Contributions.md)