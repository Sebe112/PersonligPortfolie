import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProjectService } from '../../services/project.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class AdminComponent {
  projects: any[] = [];
  newProject = { title: '', description: '', technologies: '', repositoryUrl: '' };

  constructor(private projectService: ProjectService) {}

  ngOnInit() {
    this.loadProjects();
  }

  loadProjects() {
    this.projectService.getProjects().subscribe(data => {
      this.projects = data;
    });
  }

  addProject() {
    this.projectService.createProject(this.newProject).subscribe(() => {
      this.loadProjects();
      this.newProject = { title: '', description: '', technologies: '', repositoryUrl: '' };
    });
  }

  deleteProject(id: number) {
    this.projectService.deleteProject(id).subscribe(() => this.loadProjects());
  }
}
