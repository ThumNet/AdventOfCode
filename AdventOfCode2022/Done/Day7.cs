using System.Text.Json.Serialization;

namespace AdventOfCode2022.Done;

public class Day7
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var root = new Dir("/");

        BuildDirectoryStructure(root, input);
        
        var dirUnder = FindDirs(root, 100000).ToList();
        //dirUnder.Dump(true);
        result = dirUnder.Sum(d => d.TotalSize);
        

        return result;
    }
    
    public long Challenge2(string[] input)
    {
        long result = 0;
        
        var root = new Dir("/");

        BuildDirectoryStructure(root, input);

        var max = 70000000;
        var needed = 30000000;
        var used = root.TotalSize;
        var lookfor = needed - (max - used);

        var alldirs = root.Dirs.Values.Flatten(d => d.Dirs.Values).Append(root);
        
        result = alldirs.Where(d => d.TotalSize > lookfor).OrderBy(d => d.TotalSize).First().TotalSize;


        return result;
    }

    private void BuildDirectoryStructure(Dir root, string[] input)
    {
        var current = root;
        
        for (int i = 1; i < input.Length; i++)
        {
            var line = input[i];
            if (line[0] == '$')
            {
                current = SelectFolder(current, line);
            }
            else
            {
                var parts = line.Split(" ");
                if (parts[0] == "dir")
                {
                    if (!current.Dirs.ContainsKey(parts[1]))
                    {
                        current.Dirs.Add(parts[1], new Dir(parts[1], current));
                    }
                }
                else
                {
                    current.Files.Add(parts[1], new File(parts[1], int.Parse(parts[0])));
                }
            }
        }
    }

    private IEnumerable<Dir> FindDirs(Dir dir, int maxSize)
    { 
        if (dir.TotalSize <= maxSize) yield return dir;
        foreach (var child in dir.Dirs.Values)
        {
            foreach (var childchild in FindDirs(child, maxSize))
            {
                yield return childchild;
            }
        }
    }

    private Dir SelectFolder(Dir current, string line)
    {
        if (line[2] == 'c') // cd
        {
            string dir = line[5..];
            if (dir == "..") return current.Parent;
            return current.Dirs[dir];
        }
        // skip ls

        return current;
    }

    

    public class Dir
    {
        public Dir(string name, Dir parent = null)
        {
            Name = name;
            Parent = parent;
        }
        
        public string Name { get; set; }
        
        [JsonIgnore]
        public Dir? Parent { get; set; }
        
        public Dictionary<string, File> Files { get; set; } = new();
        public Dictionary<string, Dir> Dirs { get; set; } = new();

        public long TotalSize => Dirs.Values.Sum(d => d.TotalSize) + DirSize;
        public long DirSize => Files.Values.Sum(f => f.Size);
    }

    public class File
    {
        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}